using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutritionix.Model
{
    public class Item
    {
        public object old_api_id { get; set; }
        public string item_id { get; set; }
        public string item_name { get; set; }
        public object leg_loc_id { get; set; }
        public string brand_id { get; set; }
        public string brand_name { get; set; }
        public string item_description { get; set; }
        public DateTime updated_at { get; set; }
        public object nf_ingredient_statement { get; set; }
        public object nf_water_grams { get; set; }
        public double nf_calories { get; set; }
        public object nf_calories_from_fat { get; set; }
        public int nf_total_fat { get; set; }
        public object nf_saturated_fat { get; set; }
        public object nf_trans_fatty_acid { get; set; }
        public object nf_polyunsaturated_fat { get; set; }
        public object nf_monounsaturated_fat { get; set; }
        public object nf_cholesterol { get; set; }
        public int nf_sodium { get; set; }
        public int nf_total_carbohydrate { get; set; }
        public object nf_dietary_fiber { get; set; }
        public int nf_sugars { get; set; }
        public double nf_protein { get; set; }
        public object nf_vitamin_a_dv { get; set; }
        public object nf_vitamin_c_dv { get; set; }
        public int nf_calcium_dv { get; set; }
        public int nf_iron_dv { get; set; }
        public object nf_refuse_pct { get; set; }
        public object nf_servings_per_container { get; set; }
        public int nf_serving_size_qty { get; set; }
        public string nf_serving_size_unit { get; set; }
        public object nf_serving_weight_grams { get; set; }
        public object allergen_contains_milk { get; set; }
        public object allergen_contains_eggs { get; set; }
        public object allergen_contains_fish { get; set; }
        public object allergen_contains_shellfish { get; set; }
        public object allergen_contains_tree_nuts { get; set; }
        public object allergen_contains_peanuts { get; set; }
        public object allergen_contains_wheat { get; set; }
        public object allergen_contains_soybeans { get; set; }
        public object allergen_contains_gluten { get; set; }
        public object usda_fields { get; set; }
    }
}
