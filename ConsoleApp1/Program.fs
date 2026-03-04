open System

let productOfDigits n =
    let rec loop num acc =
        let v = abs num
        if v = 0 then
            acc
        else
            loop (v / 10) (acc * (v % 10))
    
    if n = 0 then
        0
    else
        loop n 1

let rec askForNumber index total =
    printf "Введите число (%d из %d): " index total
    match Int32.TryParse(Console.ReadLine()) with
    | (true, value) -> value
    | _ -> 
        printfn "Введено не целое число."
        askForNumber index total 

let createSequence n =
    seq {
        for i in 1 .. n do
            yield askForNumber i n
    }

[<EntryPoint>]
let main _ =
    printf "Введите количество элементов: "
    match Int32.TryParse(Console.ReadLine()) with
    | (true, n) when n > 0 ->
        let originalSeq = createSequence n
        let processedSeq = originalSeq |> Seq.map productOfDigits
        processedSeq |> Seq.iteri (fun i res -> 
            printfn "Произведение цифр %d-го числа: %d" (i + 1) res)
            
    | (true, 0) -> printfn "Последовательность пуста."
    | _ -> printfn "Некорректный ввод."
    
    0