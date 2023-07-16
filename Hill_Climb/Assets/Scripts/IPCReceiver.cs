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
        // Kreirajte Named Pipe server sa odgovarajuæim imenom
        pipeServer = new NamedPipeServerStream("MyPipe");

        // Èekajte na povezivanje klijenta
        pipeServer.WaitForConnection();

        // Kreirajte èitaè za èitanje poruka iz Named Pipe-a
        reader = new StreamReader(pipeServer);
    }

    private void Update()
    {
        // Proverite da li ima dostupnih poruka
        if (pipeServer.IsConnected && reader.Peek() >= 0)
        {
            // Proèitajte poruku iz Named Pipe-a
            string message = reader.ReadLine();

            // Proverite sadržaj poruke i izvršite odgovarajuæu akciju
            if (message == "ACTIVATE_SPACE")
            {
                // Aktivirajte SPACE dugme ili izvršite željenu logiku
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
