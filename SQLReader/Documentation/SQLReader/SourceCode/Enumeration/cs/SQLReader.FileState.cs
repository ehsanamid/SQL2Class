public enum FileState : int {
    
    // Hex value is: 00000000
    [Description("The file is available for all operations. Files in the primary filegroup are alwa" +
        "ys online if the database itself is online. If a file in the primary filegroup i" +
        "s not online, the database is not online and the states of the secondary files a" +
        "re undefined.")]
    ONLINE = 0,
    
    // Hex value is: 00000001
    [Description("The file is being restored. Files enter the restoring state because of a restore " +
        "command affecting the whole file, not just a page restore, and remain in this st" +
        "ate until the restore is completed and the file is recovered.")]
    RESTORING = 1,
    
    // Hex value is: 00000002
    [Description("The file is being recovered.")]
    RECOVERING = 2,
    
    // Hex value is: 00000003
    [Description(@"The recovery of the file has been postponed. A file enters this state automatically because of a piecemeal restore process in which the file is not restored and recovered. Additional action by the user is required to resolve the error and allow for the recovery process to be completed. For more information, see Performing Piecemeal Restores.")]
    RECOVERY_PENDING = 3,
    
    // Hex value is: 00000004
    [Description(@"Recovery of the file failed during an online restore process. If the file is in the primary filegroup, the database is also marked as suspect. Otherwise, only the file is suspect and the database is still online. The file will remain in the suspect state until it is made available by one of the following methods: Restore and recovery or DBCC CHECKDB with REPAIR_ALLOW_DATA_LOSS")]
    SUSPECT = 4,
    
    // Hex value is: 00000006
    [Description(@"The file is not available for access and may not be present on the disk. Files become offline by explicit user action and remain offline until additional user action is taken. Caution: A file should only be set offline when the file is corrupted, but it can be restored. A file set to offline can only be set online by restoring the file from backup. For more information about restoring a single file, see RESTORE (Transact-SQL).")]
    OFFLINE = 6,
    
    // Hex value is: 00000007
    [Description("The file was dropped when it was not online. All files in a filegroup become defu" +
        "nct when an offline filegroup is removed.")]
    DEFUNCT = 7,
}
