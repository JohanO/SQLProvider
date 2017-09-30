﻿// Learn more about F# at http://fsharp.org
//#r "../../../bin/net461/FSharp.Data.SqlProvider.dll"
#if INTERACTIVE
#r @"C:\Program Files\dotnet\sdk\2.0.0\Microsoft\Microsoft.NET.Build.Extensions\net461\lib\netstandard.dll"
#r @"C:\Program Files\dotnet\sdk\2.0.0\Microsoft\Microsoft.NET.Build.Extensions\net461\lib\System.Reflection.dll"
#r @"C:\Program Files\dotnet\sdk\2.0.0\Microsoft\Microsoft.NET.Build.Extensions\net461\lib\System.Runtime.dll"
#r "../../../packages/System.Data.SqlClient/lib/net461/System.Data.SqlClient.dll"
#r "../../../bin/netstandard2.0/FSharp.Data.SqlProvider.dll"
#else
module Netstandard
#endif

open System
open FSharp.Data.Sql

[<Literal>]
let connStr = "Data Source=localhost; Initial Catalog=HR; Integrated Security=True"

type HR = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, connStr>

[<EntryPoint>]
let main argv =
    let runtimeConnectionString = connStr
    let ctx = HR.GetDataContext runtimeConnectionString
    let employeesFirstName = 
        query {
            for emp in ctx.Dbo.Employees do
            select emp.FirstName
        } |> Seq.head

    printfn "Hello %s!" employeesFirstName
    System.Threading.Thread.Sleep 2000
    0 // return an integer exit code
