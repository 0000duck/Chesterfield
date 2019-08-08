echo off
C:\Windows\Microsoft.NET\Framework\v4.0.30319\regasm.exe YaskawaNet.dll /unregister /codebase /nologo /silent
del /F /Q YaskawaNet.tlb
