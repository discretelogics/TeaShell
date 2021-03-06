function global:UpdateTeaShellDependencies()
{
    copy-item \\vcarl\Built\TeaFiles.Net\* . -Recurse -Verbose
}

function killpreview()
{
    get-process | where { $_.Name -eq "prevhost" } | foreach { $_.Kill(); $_ | Wait-Process }
}

function regteashell()
{
    .\RegisterExtensionDotNet40.exe -i C:\Dev\Reps\TeaFiles.Net\TeaShell\bin\Debug\DiscreteLogics.TeaShell.dll
}

function global:InstallTeaShell()
{
    Push-Location "c:\dev\Deploy\TeaShell"
    
    ## remove old version
    killpreview   
    Remove-Item * -Recurse -Verbose
    
    ## download and install new version
    copy-item \\vcarl\Built\TeaShell\* . -Recurse -Verbose
    C:\Dev\Reps\TeaShell\TeaShell\Lib\RegisterExtensionDotNet40.exe -i .\DiscreteLogics.TeaShell.dll
    
    Pop-Location
}

function global:RestartExplorer()
{
    C:\Dev\Reps\TeaShell\Tools\RestartExplorer.exe
}

function global:LocalInstallTeaShell()
{
    Push-Location "C:\Dev\Reps\TeaShell\TeaShell\bin\Release"
    
    ## remove old version
    killpreview
    
    ## download and install new version
    C:\Dev\Reps\TeaShell\Tools\RegisterExtensionDotNet40.exe -i .\DiscreteLogics.TeaShell.dll
    C:\Dev\Reps\TeaShell\Tools\RestartExplorer.exe
    
    Pop-Location
}

# test 8 