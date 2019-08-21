var TimeOff = {
    VocationalLeave : 1,
    EarlyLeave : 2,
    LeaveofAbsence : 3,
    Present : 4
};

function getEnumName(enumeration, noval) {
    for (var key in enumeration) {
        if (enumeration[key] == noval) {
            return key;
        }
    }
}