open System
open System.IO

let rec findFile (fileName: string) (currentDir: string) =
    seq {
        let files = Directory.EnumerateFiles(currentDir)
        let foundInCurrent = files |> Seq.filter (fun path -> Path.GetFileName(path) = fileName)
        yield! foundInCurrent

        let subDirs = Directory.EnumerateDirectories(currentDir)
        for dir in subDirs do
            yield! findFile fileName dir
    }

[<EntryPoint>]
let main _ =
    printf "Введите название файла (с расширением): "
    let fileName = Console.ReadLine()
    
    printf "Введите путь к начальному каталогу : "
    let startPath = Console.ReadLine()

    if Directory.Exists(startPath) then
        
        let results = findFile fileName startPath
        if Seq.isEmpty results then
            printfn "Файл '%s' не найден." fileName
        else
            printfn "Файл(ы) найдены:"
            results |> Seq.iter (printfn "- %s")
    else
        printfn "Указанный путь не существует."

    0