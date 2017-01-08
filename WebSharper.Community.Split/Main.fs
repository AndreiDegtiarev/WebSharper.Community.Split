namespace ExtSplit

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Definition =


    let Direction =
        Pattern.EnumStrings "Direction" [ "vertical";"horizontal"]
    let Cursor =
        Pattern.EnumStrings "Cursor" [ "col-resize";"row-resize"]
    let SplitProperties =
        Pattern.Config "SplitProperties" {
            Required = []
            Optional =
                [
                    "direction" , Direction.Type
                    "gutterSize",T<int>
                    "sizes",T<int[]>
                    "cursor",Cursor.Type
                ]
        } 

    let Split = Class "Split"
                |+> Static [Constructor (T<Dom.Element[]>* !?SplitProperties.Type?settings)]
    let Assembly =
        Assembly [
            Namespace "WebSharper.Community.Split" [
                Direction
                SplitProperties
                Cursor
                Split
            ]
            Namespace "WebSharper.Community.Split.Resources" [
                Resource "Extension3" "/Scripts/Split.js/Split.js"
                |> fun r -> r.AssemblyWide()
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
