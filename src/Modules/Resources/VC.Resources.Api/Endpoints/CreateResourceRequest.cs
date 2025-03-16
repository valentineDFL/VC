<<<<<<< HEAD
﻿using VC.Recources.Resource.Domain.Entities;
=======
﻿using System.Security.AccessControl;
>>>>>>> origin/feature/VC-8/ResourceModule

namespace VC.Resources.Api.Endpoints;

public record CreateResourceRequest(
    string Name,
    string Description,
    ResourceType ResourceType,
    Dictionary<string,object> Attributes
    );

