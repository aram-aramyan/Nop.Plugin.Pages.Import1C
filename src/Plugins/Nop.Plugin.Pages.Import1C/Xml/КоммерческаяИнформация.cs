
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:1C.ru:commerceml_2", IsNullable = false)]
public partial class КоммерческаяИнформация
{

    private КоммерческаяИнформацияКлассификатор классификаторField;

    private decimal версияСхемыField;

    private System.DateTime датаФормированияField;

    /// <remarks/>
    public КоммерческаяИнформацияКлассификатор Классификатор
    {
        get
        {
            return this.классификаторField;
        }
        set
        {
            this.классификаторField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal ВерсияСхемы
    {
        get
        {
            return this.версияСхемыField;
        }
        set
        {
            this.версияСхемыField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public System.DateTime ДатаФормирования
    {
        get
        {
            return this.датаФормированияField;
        }
        set
        {
            this.датаФормированияField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКлассификатор
{

    private string идField;

    private string наименованиеField;

    private КоммерческаяИнформацияКлассификаторВладелец владелецField;

    private КоммерческаяИнформацияКлассификаторГруппы группыField;

    private КоммерческаяИнформацияКлассификаторСвойство[] свойстваField;

    /// <remarks/>
    public string Ид
    {
        get
        {
            return this.идField;
        }
        set
        {
            this.идField = value;
        }
    }

    /// <remarks/>
    public string Наименование
    {
        get
        {
            return this.наименованиеField;
        }
        set
        {
            this.наименованиеField = value;
        }
    }

    /// <remarks/>
    public КоммерческаяИнформацияКлассификаторВладелец Владелец
    {
        get
        {
            return this.владелецField;
        }
        set
        {
            this.владелецField = value;
        }
    }

    /// <remarks/>
    public КоммерческаяИнформацияКлассификаторГруппы Группы
    {
        get
        {
            return this.группыField;
        }
        set
        {
            this.группыField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Свойство", IsNullable = false)]
    public КоммерческаяИнформацияКлассификаторСвойство[] Свойства
    {
        get
        {
            return this.свойстваField;
        }
        set
        {
            this.свойстваField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКлассификаторВладелец
{

    private string идField;

    private string наименованиеField;

    private string полноеНаименованиеField;

    private ulong иННField;

    /// <remarks/>
    public string Ид
    {
        get
        {
            return this.идField;
        }
        set
        {
            this.идField = value;
        }
    }

    /// <remarks/>
    public string Наименование
    {
        get
        {
            return this.наименованиеField;
        }
        set
        {
            this.наименованиеField = value;
        }
    }

    /// <remarks/>
    public string ПолноеНаименование
    {
        get
        {
            return this.полноеНаименованиеField;
        }
        set
        {
            this.полноеНаименованиеField = value;
        }
    }

    /// <remarks/>
    public ulong ИНН
    {
        get
        {
            return this.иННField;
        }
        set
        {
            this.иННField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКлассификаторГруппы
{

    private КоммерческаяИнформацияКлассификаторГруппыГруппа группаField;

    /// <remarks/>
    public КоммерческаяИнформацияКлассификаторГруппыГруппа Группа
    {
        get
        {
            return this.группаField;
        }
        set
        {
            this.группаField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКлассификаторГруппыГруппа
{

    private string идField;

    private string наименованиеField;

    private КоммерческаяИнформацияКлассификаторГруппыГруппаГруппа[] группыField;

    /// <remarks/>
    public string Ид
    {
        get
        {
            return this.идField;
        }
        set
        {
            this.идField = value;
        }
    }

    /// <remarks/>
    public string Наименование
    {
        get
        {
            return this.наименованиеField;
        }
        set
        {
            this.наименованиеField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Группа", IsNullable = false)]
    public КоммерческаяИнформацияКлассификаторГруппыГруппаГруппа[] Группы
    {
        get
        {
            return this.группыField;
        }
        set
        {
            this.группыField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКлассификаторГруппыГруппаГруппа
{

    private string идField;

    private string наименованиеField;

    private КоммерческаяИнформацияКлассификаторГруппыГруппаГруппаГруппа[] группыField;

    /// <remarks/>
    public string Ид
    {
        get
        {
            return this.идField;
        }
        set
        {
            this.идField = value;
        }
    }

    /// <remarks/>
    public string Наименование
    {
        get
        {
            return this.наименованиеField;
        }
        set
        {
            this.наименованиеField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Группа", IsNullable = false)]
    public КоммерческаяИнформацияКлассификаторГруппыГруппаГруппаГруппа[] Группы
    {
        get
        {
            return this.группыField;
        }
        set
        {
            this.группыField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКлассификаторГруппыГруппаГруппаГруппа
{

    private string идField;

    private string наименованиеField;

    private КоммерческаяИнформацияКлассификаторГруппыГруппаГруппаГруппаГруппа[] группыField;

    /// <remarks/>
    public string Ид
    {
        get
        {
            return this.идField;
        }
        set
        {
            this.идField = value;
        }
    }

    /// <remarks/>
    public string Наименование
    {
        get
        {
            return this.наименованиеField;
        }
        set
        {
            this.наименованиеField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Группа", IsNullable = false)]
    public КоммерческаяИнформацияКлассификаторГруппыГруппаГруппаГруппаГруппа[] Группы
    {
        get
        {
            return this.группыField;
        }
        set
        {
            this.группыField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКлассификаторГруппыГруппаГруппаГруппаГруппа
{

    private string идField;

    private string наименованиеField;

    private КоммерческаяИнформацияКлассификаторГруппыГруппаГруппаГруппаГруппаГруппа[] группыField;

    /// <remarks/>
    public string Ид
    {
        get
        {
            return this.идField;
        }
        set
        {
            this.идField = value;
        }
    }

    /// <remarks/>
    public string Наименование
    {
        get
        {
            return this.наименованиеField;
        }
        set
        {
            this.наименованиеField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Группа", IsNullable = false)]
    public КоммерческаяИнформацияКлассификаторГруппыГруппаГруппаГруппаГруппаГруппа[] Группы
    {
        get
        {
            return this.группыField;
        }
        set
        {
            this.группыField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКлассификаторГруппыГруппаГруппаГруппаГруппаГруппа
{

    private string идField;

    private string наименованиеField;

    /// <remarks/>
    public string Ид
    {
        get
        {
            return this.идField;
        }
        set
        {
            this.идField = value;
        }
    }

    /// <remarks/>
    public string Наименование
    {
        get
        {
            return this.наименованиеField;
        }
        set
        {
            this.наименованиеField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКлассификаторСвойство
{

    private string идField;

    private string наименованиеField;

    private string типЗначенийField;

    private КоммерческаяИнформацияКлассификаторСвойствоСправочник[] вариантыЗначенийField;

    /// <remarks/>
    public string Ид
    {
        get
        {
            return this.идField;
        }
        set
        {
            this.идField = value;
        }
    }

    /// <remarks/>
    public string Наименование
    {
        get
        {
            return this.наименованиеField;
        }
        set
        {
            this.наименованиеField = value;
        }
    }

    /// <remarks/>
    public string ТипЗначений
    {
        get
        {
            return this.типЗначенийField;
        }
        set
        {
            this.типЗначенийField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Справочник", IsNullable = false)]
    public КоммерческаяИнформацияКлассификаторСвойствоСправочник[] ВариантыЗначений
    {
        get
        {
            return this.вариантыЗначенийField;
        }
        set
        {
            this.вариантыЗначенийField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКлассификаторСвойствоСправочник
{

    private string идЗначенияField;

    private string значениеField;

    /// <remarks/>
    public string ИдЗначения
    {
        get
        {
            return this.идЗначенияField;
        }
        set
        {
            this.идЗначенияField = value;
        }
    }

    /// <remarks/>
    public string Значение
    {
        get
        {
            return this.значениеField;
        }
        set
        {
            this.значениеField = value;
        }
    }
}