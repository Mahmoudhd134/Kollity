﻿namespace Kollity.Contracts.Assignment;

public class AssignmentEditedIntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public AssignmentType Type { get; set; }
    public byte Degree { get; set; }
    public DateTime OpenUntilDate { get; set; }
}