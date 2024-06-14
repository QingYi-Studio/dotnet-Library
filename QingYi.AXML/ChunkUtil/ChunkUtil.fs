namespace ChunkUtil

open System.IO
open System

type ChunkUtil() =
    static member ReadCheckType(reader : IntReader.IntReader, expectedType : int) =
        try
            let typeValue = reader.ReadInt()
            if typeValue <> expectedType then
                let errorMessage = 
                    sprintf "Expected chunk of type 0x%X, read 0x%X." expectedType typeValue
                failwith errorMessage
        with
        | :? IOException as ex ->
            // Handle IOException if needed
            raise (new IOException("An IOException occurred.", ex))
        | ex ->
            // Handle other exceptions
            raise (new Exception("An unexpected error occurred.", ex))

