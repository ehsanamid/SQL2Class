public enum class FileState sealed : System::Int32 {
    
    // Hex value is: 00000000
    [Description(L"The file is available for all operations. Files in the primary filegroup are alwa" 
L"ys online if the database itself is online. If a file in the primary filegroup i" 
L"s not online, the database is not online and the states of the secondary files a" 
L"re undefined.")]
    ONLINE = 0,
    
    // Hex value is: 00000001
    [Description(L"The file is being restored. Files enter the restoring state because of a restore " 
L"command affecting the whole file, not just a page restore, and remain in this st" 
L"ate until the restore is completed and the file is recovered.")]
    RESTORING = 1,
    
    // Hex value is: 00000002
    [Description(L"The file is being recovered.")]
    RECOVERING = 2,
    
    // Hex value is: 00000003
    [Description(L"The recovery of the file has been postponed. A file enters this state automatical" 
L"ly because of a piecemeal restore process in which the file is not restored and " 
L"recovered. Additional action by the user is required to resolve the error and al" 
L"low for the recovery process to be completed. For more information, see Performi" 
L"ng Piecemeal Restores.")]
    RECOVERY_PENDING = 3,
    
    // Hex value is: 00000004
    [Description(L"Recovery of the file failed during an online restore process. If the file is in t" 
L"he primary filegroup, the database is also marked as suspect. Otherwise, only th" 
L"e file is suspect and the database is still online. The file will remain in the " 
L"suspect state until it is made available by one of the following methods: Restor" 
L"e and recovery or DBCC CHECKDB with REPAIR_ALLOW_DATA_LOSS")]
    SUSPECT = 4,
    
    // Hex value is: 00000006
    [Description(L"The file is not available for access and may not be present on the disk. Files be" 
L"come offline by explicit user action and remain offline until additional user ac" 
L"tion is taken. Caution: A file should only be set offline when the file is corru" 
L"pted, but it can be restored. A file set to offline can only be set online by re" 
L"storing the file from backup. For more information about restoring a single file" 
L", see RESTORE (Transact-SQL).")]
    OFFLINE = 6,
    
    // Hex value is: 00000007
    [Description(L"The file was dropped when it was not online. All files in a filegroup become defu" 
L"nct when an offline filegroup is removed.")]
    DEFUNCT = 7,
};
