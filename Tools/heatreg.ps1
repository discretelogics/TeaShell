Push-Location C:\Dev\Reps\TeaShell\Tools

C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe ..\TeaShell\bin\Release\DiscreteLogics.TeaShell.dll /regfile:teashell.reg /codebase /verbose

heat.exe reg .\teashell.reg -dr TeaShell -cg NewFilesGroup -gg -g1 -sf -srd -sreg -var "var.sources" -out ".\reg.wxs"

Pop-Location