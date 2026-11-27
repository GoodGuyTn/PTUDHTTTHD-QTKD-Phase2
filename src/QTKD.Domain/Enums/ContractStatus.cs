namespace QTKD.Domain.Enums;

/// <summary>
/// Vòng đời hợp đồng theo state machine ở tài liệu Week 02:
/// Draft → PendingApproval → (Approved → Active) | RevisionRequested → Draft | Rejected.
/// Phạm vi US03 chỉ thực hiện bước Draft → PendingApproval.
/// </summary>
public enum ContractStatus
{
    Draft,
    PendingApproval,
    RevisionRequested,
    Approved,
    Active,
    Rejected
}
