rootURL = "http://divisionsmatch.codeplex.com"
rootURL = "http://www.orientering.dk/divisionsmatch"

' find placering
Set objShell = CreateObject("Wscript.Shell")
strPath = Wscript.ScriptFullName
Set objFSO = CreateObject("Scripting.FileSystemObject")
Set objFile = objFSO.GetFile(strPath)
strFolder = objFSO.GetParentFolderName(objFile) 

' lav guid
Set TypeLib = CreateObject("Scriptlet.TypeLib")
guid = TypeLib.Guid

' find assembly version
version ="1.0.0"
strAssemblyInfo = strFolder & "\Divisionsmatch\Properties\AssemblyInfo.cs"
set f = objFSO.OpenTextFile(strAssemblyInfo)
while Not f.AtEndOfStream
	strLine = f.ReadLine
    pos = Instr(strLine, "AssemblyFileVersion")
    if pos > 0 then
        pos = Instr(strLine,"""")
        pos2 = InstrRev(strLine,"""")
        version = Mid(strLine, pos+1, pos2-pos-1)
    end if 
wend
f.close

arrVersion = split(version, ".")
textVersion = ""
for i=0 to 2
    textVersion = textVersion & arrVersion(i)
    if i<2 then
        textVersion = textVersion & "."
    end if
next

copyDest = strFolder & "\AutoUpdates\"
copySource = strFolder & "\Setup\Release\setup.msi"
if Not objFSO.FolderExists(copyDest) then
    objFSO.CreateFolder(copyDest)
end if
objFSO.CopyFile copySource, copyDest & "Divisionsmatch" & textVersion & ".msi", true


' edit vesioninfo.xml
xmlFile = copyDest & "versioninfo.xml"
Set xmlDoc = CreateObject("Msxml2.DOMDocument")
xmlDoc.Async = "False"
xmlDoc.Load(xmlFile)

Set newElement = xmlDoc.createElement("item")

Set titleElement = xmlDoc.createElement("title")
titleElement.text = "Ny verson " & textVersion & " er klar til download"
newElement.appendChild(titleElement)

Set versionElement = xmlDoc.createElement("version")
versionElement.text = version
newElement.appendChild(versionElement)

Set urlElement = xmlDoc.createElement("url")
urlElement.text = rootURL & "/Divisionsmatch" & textVersion & ".msi"
newElement.appendChild(urlElement)

Set changelogElement = xmlDoc.createElement("changelog")
changelogElement.text = rootURL & "/versionhistory.htm"
newElement.appendChild(changelogElement)

set y=xmlDoc.getElementsByTagName("item")
if (y.length > 0) then xmlDoc.removeChild y(0)
xmlDoc.appendChild newElement
xmlDoc.save(xmlFile)

'call prettyXML(xmlFile)

Function vFn_Sys_Run_CommandOutput (Command, Wait, Show, OutToFile, DeleteOutput, NoQuotes)
'Run Command similar to the command prompt, for Wait use 1 or 0. Output returned and
'stored in a file.
'Command = The command line instruction you wish to run.
'Wait = 1/0; 1 will wait for the command to finish before continuing.
'Show = 1/0; 1 will show for the command window.
'OutToFile = The file you wish to have the output recorded to.
'DeleteOutput = 1/0; 1 deletes the output file. Output is still returned to variable.
'NoQuotes = 1/0; 1 will skip wrapping the command with quotes, some commands wont work
'                if you wrap them in quotes.
'----------------------------------------------------------------------------------------
  On Error Resume Next
  'On Error Goto 0
    Set f_objShell = CreateObject("Wscript.Shell")
    Set f_objFso = CreateObject("Scripting.FileSystemObject")
    Const ForReading = 1, ForWriting = 2, ForAppending = 8
      'VARIABLES
        If OutToFile = "" Then OutToFile = "TEMP.TXT"
        tCommand = Command
        If Left(Command,1)<>"""" And NoQuotes <> 1 Then tCommand = """" & Command & """"
        tOutToFile = OutToFile
        If Left(OutToFile,1)<>"""" Then tOutToFile = """" & OutToFile & """"
        If Wait = 1 Then tWait = True
        If Wait <> 1 Then tWait = False
        If Show = 1 Then tShow = 1
        If Show <> 1 Then tShow = 0
      'RUN PROGRAM
        f_objShell.Run tCommand & " >" & tOutToFile, tShow, tWait
      'READ OUTPUT FOR RETURN
        Set f_objFile = f_objFso.OpenTextFile(OutToFile, 1)
          tMyOutput = f_objFile.ReadAll
          f_objFile.Close
          Set f_objFile = Nothing
      'DELETE FILE AND FINISH FUNCTION
        If DeleteOutput = 1 Then
          Set f_objFile = f_objFso.GetFile(OutToFile)
            f_objFile.Delete
            Set f_objFile = Nothing
          End If
        vFn_Sys_Run_CommandOutput = tMyOutput
        If Err.Number <> 0 Then vFn_Sys_Run_CommandOutput = "<0>" & Err.Description
        Err.Clear
        On Error Goto 0
      Set f_objFile = Nothing
      Set f_objShell = Nothing
  End Function


sub prettyXML (xmlFile)

    ' ****************************************

    Dim objInputFile, objOutputFile, strXML
    Dim objFSO : Set objFSO = WScript.CreateObject("Scripting.FileSystemObject")
    Dim objXML : Set objXML = WScript.CreateObject("Msxml2.DOMDocument")
    Dim objXSL : Set objXSL = WScript.CreateObject("Msxml2.DOMDocument")

    ' ****************************************
    ' Put whitespace between tags. (Required for XSL transformation.)
    ' ****************************************

    Set objInputFile = objFSO.OpenTextFile(xmlFile,1,False,-2)
    strXML = objInputFile.ReadAll
    objInputFile.Close
    
    wscript.echo strXML

    Set objOutputFile = objFSO.CreateTextFile(xmlFile,True,False)
    strXML = Replace(strXML,"><",">" & vbCrLf & "<")
    objOutputFile.Write strXML
    objOutputFile.Close

	wscript.echo strXML
    ' ****************************************
    ' Create an XSL stylesheet for transformation.
    ' ****************************************

    Dim strStylesheet : strStylesheet = _
        "<xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">" & _
        "<xsl:output method=""xml"" indent=""yes""/>" & _
        "<xsl:template match=""/"">" & _
        "<xsl:copy-of select="".""/>" & _
        "</xsl:template>" & _
        "</xsl:stylesheet>"

    ' ****************************************
    ' Transform the XML.
    ' ****************************************

'    objXSL.loadXML strStylesheet
'    objXML.load xmlFile
'    objXML.transformNode objXSL
'    objXML.save xmlFile & ".xml"

end sub