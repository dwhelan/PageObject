using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    public abstract class ElementTest<TP, TE> : BaseElementTest<TP, TE> where TP : TestPage<TE> where TE : Element
    {
        protected Coypu.Element CoypuElement => Element.CoypuElement;
    }
}
