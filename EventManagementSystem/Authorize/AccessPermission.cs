using Constracts.DTO;
using Domain.Enum;

namespace Web.Authorize
{
    /// <summary>
    /// Dictionary contain permission of user
    /// <para>Permission (key, EPermission) and can access (value, bool)</para>
    /// </summary>
    public class AccessPermission : Dictionary<EPermission, bool>
    {
        private readonly List<EPermission> organizerPermission =
        [
            EPermission.Statistics,
            EPermission.Event,
            EPermission.Booking
        ];

        public AccessPermission(UserDTO user)
        {
            GrantPermissions(user);
        }

        #region Prevent Public Modified

        public new bool this[EPermission key]
        {
            get => base[key];
            private set => base[key] = value;
        }

        [Obsolete("This function not working cause this class not allow modified value")]
        public new void Add(EPermission key, bool value)
        {
            throw new NotSupportedException("This function not working cause this class not allow modified value");
        }

        [Obsolete("This function not working cause this class not allow modified value")]
        public new void Remove(EPermission key)
        {
            throw new NotSupportedException("This function not working cause this class not allow modified value");
        }

        [Obsolete("This function not working cause this class not allow modified value")]
        public new void Clear()
        {
            throw new NotSupportedException("This function not working cause this class not allow modified value");
        }

        #endregion

        private void GrantPermissions(UserDTO user)
        {
            ResetPermissions();
            if (user == null) return;

            string role = user.Role!;

            switch (role)
            {
                case "Administrator":
                    GrantAdminPermission();
                    break;

                case "Organizer":
                    GrantOrganizerPermission();
                    break;

                default:
                    break;
            }
        }

        private void ResetPermissions()
        {
            this[EPermission.Statistics] = false;
            this[EPermission.Event] = false;
            this[EPermission.Booking] = false;
            this[EPermission.User] = false;
            this[EPermission.Site] = false;
            this[EPermission.Contact] = false;
            this[EPermission.Setting] = false;
        }

        private void GrantAdminPermission()
        {
            foreach (var key in Keys)
            {
                this[key] = true;
            }
        }

        private void GrantOrganizerPermission()
        {
            foreach (var func in organizerPermission)
            {
                this[func] = true;
            }
        }
    }
}
