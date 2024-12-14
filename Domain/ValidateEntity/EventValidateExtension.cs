using Domain.CustomAttribute;

namespace Domain.ValidateEntity
{
    public static class EntityValidateExtension
    {
        public static bool NullOrEmptyValidate(this object model)
        {
            foreach (var propertyInfor in model.GetType().GetProperties()) {
                bool allowNull = propertyInfor.GetCustomAttributes(typeof(AllowEmptyOrNullAttribute), true).FirstOrDefault() != null;
                if (allowNull) continue;

                var propertyValue = propertyInfor.GetValue(model);

                if (propertyValue is string str && str == "") return false;
                else if (propertyValue == null) return false;
            }

            return true;
        }
    }
}
