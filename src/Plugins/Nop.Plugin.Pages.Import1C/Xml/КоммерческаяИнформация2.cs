
/// <remarks/>
public partial class КоммерческаяИнформация
{
    private КоммерческаяИнформацияКаталог каталогField;

    /// <remarks/>
    public КоммерческаяИнформацияКаталог Каталог
    {
        get
        {
            return this.каталогField;
        }
        set
        {
            this.каталогField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКаталог
{
    private string идField;

    private string идКлассификатораField;

    private string наименованиеField;

    private КоммерческаяИнформацияКаталогВладелец владелецField;

    private КоммерческаяИнформацияКаталогТовар[] товарыField;

    private bool содержитТолькоИзмененияField;

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
    public string ИдКлассификатора
    {
        get
        {
            return this.идКлассификатораField;
        }
        set
        {
            this.идКлассификатораField = value;
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
    public КоммерческаяИнформацияКаталогВладелец Владелец
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
    [System.Xml.Serialization.XmlArrayItemAttribute("Товар", IsNullable = false)]
    public КоммерческаяИнформацияКаталогТовар[] Товары
    {
        get
        {
            return this.товарыField;
        }
        set
        {
            this.товарыField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool СодержитТолькоИзменения
    {
        get
        {
            return this.содержитТолькоИзмененияField;
        }
        set
        {
            this.содержитТолькоИзмененияField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКаталогВладелец
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
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКаталогТовар
{

    private string идField;

    private string артикулField;

    private string наименованиеField;

    private КоммерческаяИнформацияКаталогТоварБазоваяЕдиница базоваяЕдиницаField;

    private КоммерческаяИнформацияКаталогТоварГруппы группыField;

    private string описаниеField;

    private КоммерческаяИнформацияКаталогТоварИзготовитель изготовительField;

    private КоммерческаяИнформацияКаталогТоварЗначенияСвойства[] значенияСвойствField;

    private КоммерческаяИнформацияКаталогТоварСтавкиНалогов ставкиНалоговField;

    private КоммерческаяИнформацияКаталогТоварЗначениеРеквизита[] значенияРеквизитовField;

    private string статусField;

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
    public string Артикул
    {
        get
        {
            return this.артикулField;
        }
        set
        {
            this.артикулField = value;
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
    public КоммерческаяИнформацияКаталогТоварБазоваяЕдиница БазоваяЕдиница
    {
        get
        {
            return this.базоваяЕдиницаField;
        }
        set
        {
            this.базоваяЕдиницаField = value;
        }
    }

    /// <remarks/>
    public КоммерческаяИнформацияКаталогТоварГруппы Группы
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
    public string Описание
    {
        get
        {
            return this.описаниеField;
        }
        set
        {
            this.описаниеField = value;
        }
    }

    /// <remarks/>
    public КоммерческаяИнформацияКаталогТоварИзготовитель Изготовитель
    {
        get
        {
            return this.изготовительField;
        }
        set
        {
            this.изготовительField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("ЗначенияСвойства", IsNullable = false)]
    public КоммерческаяИнформацияКаталогТоварЗначенияСвойства[] ЗначенияСвойств
    {
        get
        {
            return this.значенияСвойствField;
        }
        set
        {
            this.значенияСвойствField = value;
        }
    }

    /// <remarks/>
    public КоммерческаяИнформацияКаталогТоварСтавкиНалогов СтавкиНалогов
    {
        get
        {
            return this.ставкиНалоговField;
        }
        set
        {
            this.ставкиНалоговField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("ЗначениеРеквизита", IsNullable = false)]
    public КоммерческаяИнформацияКаталогТоварЗначениеРеквизита[] ЗначенияРеквизитов
    {
        get
        {
            return this.значенияРеквизитовField;
        }
        set
        {
            this.значенияРеквизитовField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Статус
    {
        get
        {
            return this.статусField;
        }
        set
        {
            this.статусField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКаталогТоварБазоваяЕдиница
{

    private КоммерческаяИнформацияКаталогТоварБазоваяЕдиницаПересчет пересчетField;

    private ushort кодField;

    private string наименованиеПолноеField;

    private string международноеСокращениеField;

    /// <remarks/>
    public КоммерческаяИнформацияКаталогТоварБазоваяЕдиницаПересчет Пересчет
    {
        get
        {
            return this.пересчетField;
        }
        set
        {
            this.пересчетField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort Код
    {
        get
        {
            return this.кодField;
        }
        set
        {
            this.кодField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string НаименованиеПолное
    {
        get
        {
            return this.наименованиеПолноеField;
        }
        set
        {
            this.наименованиеПолноеField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string МеждународноеСокращение
    {
        get
        {
            return this.международноеСокращениеField;
        }
        set
        {
            this.международноеСокращениеField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКаталогТоварБазоваяЕдиницаПересчет
{

    private ushort единицаField;

    private byte коэффициентField;

    /// <remarks/>
    public ushort Единица
    {
        get
        {
            return this.единицаField;
        }
        set
        {
            this.единицаField = value;
        }
    }

    /// <remarks/>
    public byte Коэффициент
    {
        get
        {
            return this.коэффициентField;
        }
        set
        {
            this.коэффициентField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКаталогТоварГруппы
{

    private string идField;

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
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКаталогТоварИзготовитель
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
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКаталогТоварЗначенияСвойства
{

    private string идField;

    private string значениеField;

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

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКаталогТоварСтавкиНалогов
{

    private КоммерческаяИнформацияКаталогТоварСтавкиНалоговСтавкаНалога ставкаНалогаField;

    /// <remarks/>
    public КоммерческаяИнформацияКаталогТоварСтавкиНалоговСтавкаНалога СтавкаНалога
    {
        get
        {
            return this.ставкаНалогаField;
        }
        set
        {
            this.ставкаНалогаField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКаталогТоварСтавкиНалоговСтавкаНалога
{

    private string наименованиеField;

    private byte ставкаField;

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
    public byte Ставка
    {
        get
        {
            return this.ставкаField;
        }
        set
        {
            this.ставкаField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияКаталогТоварЗначениеРеквизита
{

    private string наименованиеField;

    private string значениеField;

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