# Delete temporary files
Service that deletes the temp files in the computer

Delete temp files is a Windows service that delete the temp files from the computer.
The deletion of the files is once a day - the time can be changed via Right click on the solution -> Properties -> Settings -> ExecuteTime.
In Settings it is also specified an interval by which the service will check if the deletion is already done. 

The project contains 3 files - DeleteClass, ProjectInstaller and Sheduler.  
-> DeleteClass contains 3 methods - one that performs the deletion of the files and two that write messages in a log file.  
-> ProjectInstaller has one serviceInstaller which specifies the ServiceName, the StartType (Boot, System, Automatic, Manual or Disabled),  and one serviceProcessInstaller which specifies the Account (LocalService, NetworkService, LocalSystem or User).  
-> Sheduler contains the override methods OnStart() and OnStop() and timer_Tick() method.
