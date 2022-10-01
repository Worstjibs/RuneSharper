using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Shared.Settings;

public class JwtTokenSettings
{
    public string SecretKey { get; init; } = default!;
}
