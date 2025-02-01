using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using TestTasks.InternationalTradeTask.Models;

namespace TestTasks.InternationalTradeTask
{
    public class CommodityRepository
    {
        public double GetImportTariff(string commodityName)
        {
            double? found = FindCommodityGroupImportTarifByName(_allCommodityGroups, commodityName, null);
            if(found == null)
            {
                throw new ArgumentException();
            }
            return (double)found;
        }

        public double GetExportTariff(string commodityName)
        {
            double? found = FindCommodityGroupExportTarifByName(_allCommodityGroups, commodityName, null);
            if (found == null)
            {
                throw new ArgumentException();
            }
            return (double)found;
        }

        private double? FindCommodityGroupImportTarifByName(ICommodityGroup[] groups, string commodityName, double? tarif)
        {
            foreach (ICommodityGroup item in groups)
            {
                if(item.ImportTarif != null)
                {
                    tarif = item.ImportTarif;
                }
                if (item.Name == commodityName)
                    return item.ImportTarif == null ? tarif : item.ImportTarif;
                else
                {
                    if(item.SubGroups != null)
                    {
                        var found = FindCommodityGroupImportTarifByName(item.SubGroups, commodityName, tarif);
                        if (found != null)
                        {
                            return found;
                        }
                    }
                }
            }
            return null;
        }

        private double? FindCommodityGroupExportTarifByName(ICommodityGroup[] groups, string commodityName, double? tarif)
        {
            foreach (ICommodityGroup item in groups)
            {
                if (item.ExportTarif != null)
                {
                    tarif = item.ExportTarif;
                }
                if (item.Name == commodityName)
                    return item.ExportTarif == null ? tarif : item.ExportTarif;
                else
                {
                    if (item.SubGroups != null)
                    {
                        var found = FindCommodityGroupExportTarifByName(item.SubGroups, commodityName, tarif);
                        if (found != null)
                        {
                            return found;
                        }
                    }
                }
            }
            return null;
        }

        private FullySpecifiedCommodityGroup[] _allCommodityGroups = new FullySpecifiedCommodityGroup[]
        {
            new FullySpecifiedCommodityGroup("06", "Sugar, sugar preparations and honey", 0.05, 0)
            {
                SubGroups = new CommodityGroup[]
                {
                    new CommodityGroup("061", "Sugar and honey")
                    {
                        SubGroups = new CommodityGroup[]
                        {
                            new CommodityGroup("0611", "Raw sugar,beet & cane"),
                            new CommodityGroup("0612", "Refined sugar & other prod.of refining,no syrup"),
                            new CommodityGroup("0615", "Molasses", 0, 0),
                            new CommodityGroup("0616", "Natural honey", 0, 0),
                            new CommodityGroup("0619", "Sugars & syrups nes incl.art.honey & caramel"),
                        }
                    },
                    new CommodityGroup("062", "Sugar confy, sugar preps. Ex chocolate confy", 0, 0)
                }
            },
            new FullySpecifiedCommodityGroup("282", "Iron and steel scrap", 0, 0.1)
            {
                SubGroups = new CommodityGroup[]
                {
                    new CommodityGroup("28201", "Iron/steel scrap not sorted or graded"),
                    new CommodityGroup("28202", "Iron/steel scrap sorted or graded/cast iron"),
                    new CommodityGroup("28203", "Iron/steel scrap sort.or graded/tinned iron"),
                    new CommodityGroup("28204", "Rest of 282.0")
                }
            }
        };
    }
}
