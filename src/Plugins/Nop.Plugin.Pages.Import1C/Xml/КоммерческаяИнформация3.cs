/// <remarks/>
public partial class КоммерческаяИнформация
{

    private КоммерческаяИнформацияПакетПредложений пакетПредложенийField;
    /// <remarks/>
    public КоммерческаяИнформацияПакетПредложений ПакетПредложений
    {
        get
        {
            return this.пакетПредложенийField;
        }
        set
        {
            this.пакетПредложенийField = value;
        }
    }


}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияПакетПредложений
{

    private string идField;

    private string наименованиеField;

    private string идКаталогаField;

    private string идКлассификатораField;

    private КоммерческаяИнформацияПакетПредложенийВладелец владелецField;

    private КоммерческаяИнформацияПакетПредложенийТипыЦен типыЦенField;

    private КоммерческаяИнформацияПакетПредложенийСклады складыField;

    private КоммерческаяИнформацияПакетПредложенийПредложение[] предложенияField;

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
    public string ИдКаталога
    {
        get
        {
            return this.идКаталогаField;
        }
        set
        {
            this.идКаталогаField = value;
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
    public КоммерческаяИнформацияПакетПредложенийВладелец Владелец
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
    public КоммерческаяИнформацияПакетПредложенийТипыЦен ТипыЦен
    {
        get
        {
            return this.типыЦенField;
        }
        set
        {
            this.типыЦенField = value;
        }
    }

    /// <remarks/>
    public КоммерческаяИнформацияПакетПредложенийСклады Склады
    {
        get
        {
            return this.складыField;
        }
        set
        {
            this.складыField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Предложение", IsNullable = false)]
    public КоммерческаяИнформацияПакетПредложенийПредложение[] Предложения
    {
        get
        {
            return this.предложенияField;
        }
        set
        {
            this.предложенияField = value;
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияПакетПредложенийВладелец
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
public partial class КоммерческаяИнформацияПакетПредложенийТипыЦен
{

    private КоммерческаяИнформацияПакетПредложенийТипыЦенТипЦены типЦеныField;

    /// <remarks/>
    public КоммерческаяИнформацияПакетПредложенийТипыЦенТипЦены ТипЦены
    {
        get
        {
            return this.типЦеныField;
        }
        set
        {
            this.типЦеныField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияПакетПредложенийТипыЦенТипЦены
{

    private string идField;

    private string наименованиеField;

    private string валютаField;

    private КоммерческаяИнформацияПакетПредложенийТипыЦенТипЦеныНалог налогField;

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
    public string Валюта
    {
        get
        {
            return this.валютаField;
        }
        set
        {
            this.валютаField = value;
        }
    }

    /// <remarks/>
    public КоммерческаяИнформацияПакетПредложенийТипыЦенТипЦеныНалог Налог
    {
        get
        {
            return this.налогField;
        }
        set
        {
            this.налогField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияПакетПредложенийТипыЦенТипЦеныНалог
{

    private string наименованиеField;

    private bool учтеноВСуммеField;

    private bool акцизField;

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
    public bool УчтеноВСумме
    {
        get
        {
            return this.учтеноВСуммеField;
        }
        set
        {
            this.учтеноВСуммеField = value;
        }
    }

    /// <remarks/>
    public bool Акциз
    {
        get
        {
            return this.акцизField;
        }
        set
        {
            this.акцизField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияПакетПредложенийСклады
{

    private КоммерческаяИнформацияПакетПредложенийСкладыСклад складField;

    /// <remarks/>
    public КоммерческаяИнформацияПакетПредложенийСкладыСклад Склад
    {
        get
        {
            return this.складField;
        }
        set
        {
            this.складField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияПакетПредложенийСкладыСклад
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
public partial class КоммерческаяИнформацияПакетПредложенийПредложение
{

    private string идField;

    private string артикулField;

    private string наименованиеField;

    private КоммерческаяИнформацияПакетПредложенийПредложениеБазоваяЕдиница базоваяЕдиницаField;

    private КоммерческаяИнформацияПакетПредложенийПредложениеЦены ценыField;

    private byte количествоField;

    private КоммерческаяИнформацияПакетПредложенийПредложениеСклад складField;

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
    public КоммерческаяИнформацияПакетПредложенийПредложениеБазоваяЕдиница БазоваяЕдиница
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
    public КоммерческаяИнформацияПакетПредложенийПредложениеЦены Цены
    {
        get
        {
            return this.ценыField;
        }
        set
        {
            this.ценыField = value;
        }
    }

    /// <remarks/>
    public byte Количество
    {
        get
        {
            return this.количествоField;
        }
        set
        {
            this.количествоField = value;
        }
    }

    /// <remarks/>
    public КоммерческаяИнформацияПакетПредложенийПредложениеСклад Склад
    {
        get
        {
            return this.складField;
        }
        set
        {
            this.складField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияПакетПредложенийПредложениеБазоваяЕдиница
{

    private КоммерческаяИнформацияПакетПредложенийПредложениеБазоваяЕдиницаПересчет пересчетField;

    private ushort кодField;

    private string наименованиеПолноеField;

    private string международноеСокращениеField;

    /// <remarks/>
    public КоммерческаяИнформацияПакетПредложенийПредложениеБазоваяЕдиницаПересчет Пересчет
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияПакетПредложенийПредложениеБазоваяЕдиницаПересчет
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияПакетПредложенийПредложениеЦены
{

    private КоммерческаяИнформацияПакетПредложенийПредложениеЦеныЦена ценаField;

    /// <remarks/>
    public КоммерческаяИнформацияПакетПредложенийПредложениеЦеныЦена Цена
    {
        get
        {
            return this.ценаField;
        }
        set
        {
            this.ценаField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияПакетПредложенийПредложениеЦеныЦена
{

    private string представлениеField;

    private string идТипаЦеныField;

    private decimal ценаЗаЕдиницуField;

    private string валютаField;

    private string единицаField;

    private byte коэффициентField;

    /// <remarks/>
    public string Представление
    {
        get
        {
            return this.представлениеField;
        }
        set
        {
            this.представлениеField = value;
        }
    }

    /// <remarks/>
    public string ИдТипаЦены
    {
        get
        {
            return this.идТипаЦеныField;
        }
        set
        {
            this.идТипаЦеныField = value;
        }
    }

    /// <remarks/>
    public decimal ЦенаЗаЕдиницу
    {
        get
        {
            return this.ценаЗаЕдиницуField;
        }
        set
        {
            this.ценаЗаЕдиницуField = value;
        }
    }

    /// <remarks/>
    public string Валюта
    {
        get
        {
            return this.валютаField;
        }
        set
        {
            this.валютаField = value;
        }
    }

    /// <remarks/>
    public string Единица
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:1C.ru:commerceml_2")]
public partial class КоммерческаяИнформацияПакетПредложенийПредложениеСклад
{

    private string идСкладаField;

    private int количествоНаСкладеField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ИдСклада
    {
        get
        {
            return this.идСкладаField;
        }
        set
        {
            this.идСкладаField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int КоличествоНаСкладе
    {
        get
        {
            return this.количествоНаСкладеField;
        }
        set
        {
            this.количествоНаСкладеField = value;
        }
    }
}
