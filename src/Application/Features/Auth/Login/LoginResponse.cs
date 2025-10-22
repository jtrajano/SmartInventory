using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Features.Auth.Login;
public record LoginResponse(string Token , DateTime ExpireAt);
