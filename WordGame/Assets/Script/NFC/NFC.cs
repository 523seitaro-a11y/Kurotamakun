using System;
using System.Text;
using UnityEngine;
using PCSC;
using System.Collections;

public class NFCReader : MonoBehaviour
{
    public bool isCardPresent = false;

    public bool isH = false;
    public bool isM = false;
    public bool isP = false;

    private bool previousState = false;

    void Update()
    {
        CheckCardState();
    }

    void CheckCardState()
    {
        try
        {
            using (var context = ContextFactory.Instance.Establish(SCardScope.System))
            {
                var readers = context.GetReaders();
                if (readers == null || readers.Length == 0)
                {
                    isCardPresent = false;
                    return;
                }

                using (var reader = new SCardReader(context))
                {
                    var rc = reader.Connect(
                        readers[0],
                        SCardShareMode.Shared,
                        SCardProtocol.Any
                    );

                    isCardPresent = (rc == SCardError.Success);

                    if (isCardPresent)
                        reader.Disconnect(SCardReaderDisposition.Leave);
                }
            }

            // 🔹 カードが置かれた瞬間
            if (isCardPresent && !previousState)
            {
                ReadNDEF();
            }

            // 🔹 カードが外れた瞬間
            if (!isCardPresent && previousState)
            {
                isH = false;
                isM = false;
                isP = false;
            }

            previousState = isCardPresent;
        }
        catch
        {
            isCardPresent = false;
        }
    }

    void ReadNDEF()
    {
        try
        {
            using (var context = ContextFactory.Instance.Establish(SCardScope.System))
            {
                var readers = context.GetReaders();
                if (readers == null || readers.Length == 0) return;

                using (var reader = new SCardReader(context))
                {
                    var rc = reader.Connect(
                        readers[0],
                        SCardShareMode.Shared,
                        SCardProtocol.Any
                    );

                    if (rc != SCardError.Success)
                        return;

                    var sendPci = SCardPCI.GetPci(reader.ActiveProtocol);
                    var receivePci = new SCardPCI();

                    byte[] readCmd = new byte[]
                    {
                        0xFF, 0xB0, 0x00, 0x04, 0x10
                    };

                    byte[] receiveBuffer = new byte[256];
                    int receiveLength = receiveBuffer.Length;

                    var rcTransmit = reader.Transmit(
                        sendPci,
                        readCmd,
                        readCmd.Length,
                        receivePci,
                        receiveBuffer,
                        ref receiveLength
                    );

                    if (rcTransmit != SCardError.Success)
                        return;

                    string text = ExtractTextFromNDEF(receiveBuffer, receiveLength);

                    // 一旦リセット
                    isH = false;
                    isM = false;
                    isP = false;

                    if (text == "h") isH = true;
                    if (text == "m") isM = true;
                    if (text == "p") isP = true;


                    reader.Disconnect(SCardReaderDisposition.Leave);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    string ExtractTextFromNDEF(byte[] buffer, int length)
    {
        try
        {
            for (int i = 0; i < length; i++)
            {
                if (buffer[i] == 0x03)
                {
                    int ndefLength = buffer[i + 1];
                    int index = i + 2;

                    for (int j = index; j < index + ndefLength; j++)
                    {
                        if (buffer[j] == 0x54)
                        {
                            int payloadLength = buffer[j - 1];
                            int langLength = buffer[j + 1] & 0x3F;

                            int textStart = j + 2 + langLength;
                            int textLength = payloadLength - 1 - langLength;

                            return Encoding.UTF8.GetString(buffer, textStart, textLength);
                        }
                    }
                }
            }
        }
        catch { }

        return "";
    }
}