module Login.View

open Fable.Core
open Fable.Import
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Client.Style
open System
open Fable.Core.JsInterop

open Login.Types

let [<Literal>] ENTER_KEY = 13.

let root model dispatch =
    let showErrorClass = if String.IsNullOrEmpty model.ErrorMsg then "hidden" else ""
    let buttonActive = if String.IsNullOrEmpty model.Login.UserName || String.IsNullOrEmpty model.Login.Password then "btn-disabled" else "btn-primary"

    let onEnter msg dispatch =
        function
        | (ev:React.KeyboardEvent) when ev.keyCode = ENTER_KEY ->
            ev.preventDefault()
            dispatch msg
        | _ -> ()
        |> OnKeyDown

    match model.State with
    | LoggedIn _ ->
        div [Id "greeting"] [
          h3 [ ClassName "text-center" ] [ str (sprintf "Hi %s!" model.Login.UserName) ]
        ]

    | LoggedOut ->
        div [ClassName "signInBox" ] [
          h3 [ ClassName "text-center" ] [ str "Log in with 'test' / 'test'."]

          div [ ClassName showErrorClass ] [
                  div [ ClassName "alert alert-danger" ] [ str model.ErrorMsg ]
           ]

          div [ ClassName "input-group input-group-lg" ] [
                span [ClassName "input-group-addon" ] [
                  span [ClassName "glyphicon glyphicon-user"] []
                ]
                input [
                    Id "username"
                    HTMLAttr.Type "text"
                    ClassName "form-control input-lg"
                    Placeholder "Username"
                    DefaultValue model.Login.UserName
                    OnChange (fun ev -> dispatch (SetUserName !!ev.target?value))
                    AutoFocus true ]
          ]

          div [ ClassName "input-group input-group-lg" ] [
                span [ClassName "input-group-addon" ] [
                  span [ClassName "glyphicon glyphicon-asterisk"] []
                ]
                input [
                        Id "password"
                        HTMLAttr.Type "password"
                        ClassName "form-control input-lg"
                        Placeholder "Password"
                        DefaultValue model.Login.Password
                        OnChange (fun ev -> dispatch (SetPassword !!ev.target?value))
                        onEnter ClickLogIn dispatch  ]
            ]

          div [ ClassName "text-center" ] [
              button [ ClassName ("btn " + buttonActive); OnClick (fun _ -> dispatch ClickLogIn) ] [ str "Log In" ]
          ]
        ]
