using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xunit;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
internal sealed class TestCategoryAttribute : Attribute
{
    public string Category { get; set; }

    public TestCategoryAttribute(string category)
    {
        Category = category;
    }
}
