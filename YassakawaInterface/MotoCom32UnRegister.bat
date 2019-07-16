echo off
C:\Windows\Microsoft.NET\Framework\v4.0.30319\regasm.exe MotoCom32Net.dll /unregister /codebase /nologo /silent
del /F /Q MotoCom32Net.tlb
