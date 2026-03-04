open System

let tryParseBinary (input: string) =
    try
        let value = Convert.ToInt32(input, 2)
        if value >= 1 && value <= 9 then
            Some(value)
        else
            None
    with _ -> None


let rec getValidBinaryInput index total =
    printf "\nВведите двоичное число 1-9 (элемент %d из %d): " index total
    let input = Console.ReadLine()
    match tryParseBinary input with
    | Some(value) -> value
    | None -> 
        printfn "Введите корректное двоичное число от 1 до 9."
        getValidBinaryInput index total

[<EntryPoint>]
let main _ =
    printf "Введите количество элементов: "
    match Int32.TryParse(Console.ReadLine()) with
    | (true, n) when n > 0 ->
        let binaryNumbers = 
            Seq.init n (fun i -> getValidBinaryInput (i + 1) n)
            |> Seq.cache

        printfn "\nПоследовательность:"
        binaryNumbers |> Seq.iter (printf "%d ")
        printfn "\n" 

        let totalSum = binaryNumbers |> Seq.fold (fun acc x -> acc + x) 0
        
        printfn "Итоговая сумма: %d" totalSum
        
    | _ -> printfn "Некорректное количество элементов."
    
    0