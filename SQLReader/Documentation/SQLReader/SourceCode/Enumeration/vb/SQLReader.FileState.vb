Public Enum FileState As Integer
    
    'Hex value is: 00000000
    <Description("The file is available for all operations. Files in the primary filegroup are alwa"& _ 
        "ys online if the database itself is online. If a file in the primary filegroup i"& _ 
        "s not online, the database is not online and the states of the secondary files a"& _ 
        "re undefined.")>  _
    ONLINE = 0
    
    'Hex value is: 00000001
    <Description("The file is being restored. Files enter the restoring state because of a restore "& _ 
        "command affecting the whole file, not just a page restore, and remain in this st"& _ 
        "ate until the restore is completed and the file is recovered.")>  _
    RESTORING = 1
    
    'Hex value is: 00000002
    <Description("The file is being recovered.")>  _
    RECOVERING = 2
    
    'Hex value is: 00000003
    <Description("The recovery of the file has been postponed. A file enters this state automatical"& _ 
        "ly because of a piecemeal restore process in which the file is not restored and "& _ 
        "recovered. Additional action by the user is required to resolve the error and al"& _ 
        "low for the recovery process to be completed. For more information, see Performi"& _ 
        "ng Piecemeal Restores.")>  _
    RECOVERY_PENDING = 3
    
    'Hex value is: 00000004
    <Description("Recovery of the file failed during an online restore process. If the file is in t"& _ 
        "he primary filegroup, the database is also marked as suspect. Otherwise, only th"& _ 
        "e file is suspect and the database is still online. The file will remain in the "& _ 
        "suspect state until it is made available by one of the following methods: Restor"& _ 
        "e and recovery or DBCC CHECKDB with REPAIR_ALLOW_DATA_LOSS")>  _
    SUSPECT = 4
    
    'Hex value is: 00000006
    <Description("The file is not available for access and may not be present on the disk. Files be"& _ 
        "come offline by explicit user action and remain offline until additional user ac"& _ 
        "tion is taken. Caution: A file should only be set offline when the file is corru"& _ 
        "pted, but it can be restored. A file set to offline can only be set online by re"& _ 
        "storing the file from backup. For more information about restoring a single file"& _ 
        ", see RESTORE (Transact-SQL).")>  _
    OFFLINE = 6
    
    'Hex value is: 00000007
    <Description("The file was dropped when it was not online. All files in a filegroup become defu"& _ 
        "nct when an offline filegroup is removed.")>  _
    DEFUNCT = 7
End Enum
