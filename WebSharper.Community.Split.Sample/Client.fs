namespace WebSharper.Community.Split.Sample

open WebSharper
open WebSharper.JavaScript
open WebSharper.Html.Client
open WebSharper.Community.Split

[<JavaScript>]
module Client =

    let Start input k =
        async {
            let! data = Server.DoSomething input
            return k data
        }
        |> Async.Start

    let Main () =
        let leftTopDiv=Div[Attr.Class "split content"]
        let leftBottomDiv=Div[Attr.Class "split content"]
        let leftDiv=Div[]
                    -<[Attr.Class "split split-horizontal"]
                    -<[leftTopDiv]
                    -<[leftBottomDiv]
                 
        let rightTopDiv=Div[Attr.Class "split content"]
        let rightBottomDiv=Div[Attr.Class "split content"]
        let rightDiv=Div[]
                    -<[Attr.Class "split split-horizontal"]
                    -<[rightTopDiv]
                    -<[rightBottomDiv]
        Div [ 
             //Attr.Class "content" :?> Element
             Attr.Style "height:570px" :?> Element
             leftDiv
             rightDiv
        ]|>! OnAfterRender (fun div ->
                                let settingsVert=SplitProperties(
                                                                Direction=Direction.Horizontal,
                                                                GutterSize=3,
                                                                Sizes=[|22; 78|],
                                                                Cursor=Cursor.Col_resize
                                                                )
                                let splt1=new Split([|leftDiv.Dom;rightDiv.Dom|],settingsVert)

                                let settingsHor=SplitProperties(
                                                                Direction=Direction.Vertical,
                                                                GutterSize=3,
                                                                Sizes=[|70; 30|],
                                                                Cursor=Cursor.Row_resize
                                                                )
                                let splt2=new Split([|leftTopDiv.Dom;leftBottomDiv.Dom|],settingsHor)
                                let splt3=new Split([|rightTopDiv.Dom;rightBottomDiv.Dom|],settingsHor)
                                ()
                           )