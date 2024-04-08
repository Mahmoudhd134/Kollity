﻿using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Assignment;

namespace Kollity.Services.Application.Queries.Assignment.GetGroupDegree;

public record GetAssignmentGroupMembersAnswerDegreeQuery(Guid AssignmentId, Guid GroupId)
    : IQuery<AssignmentGroupDegreeDto>;