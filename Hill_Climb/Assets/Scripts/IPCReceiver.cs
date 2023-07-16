using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.IO.Pipes;

public class IPCReceiver : MonoBehaviour
{
    private NamedPipeServerStream pipeServer;
    private StreamReader reader;

    private void Start()
    {
        // Kreirajte Named Pipe server sa odgovaraju�im imenom
        pipeServer = new NamedPipeServerStream("MyPipe");

        // �ekajte na povezivanje klijenta
        pipeServer.WaitForConnection();

        // Kreirajte �ita� za �itanje poruka iz Named Pipe-a
        reader = new StreamReader(pipeServer);
    }

    private void Update()
    {
        // Proverite da li ima dostupnih poruka
        if (pipeServer.IsConnected && reader.Peek() >= 0)
        {
            // Pro�itajte poruku iz Named Pipe-a
            string message = reader.ReadLine();

            // Proverite sadr�aj poruke i izvr�ite odgovaraju�u akciju
            if (message == "ACTIVATE_SPACE")
            {
                // Aktivirajte SPACE dugme ili izvr�ite �eljenu logiku
                // npr.:
                Input.GetKeyDown(KeyCode.Space);
            }
        }
    }

    private void OnDestroy()
    {
        // Zatvorite Named Pipe server i oslobodite resurse
        reader.Close();
        pipeServer.Close();
    }
}
