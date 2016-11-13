using System;


namespace EntityModels
{
    public static class RolesKind
    {
        public const string PATIENT = "pacjent";
        public const string ADMIN = "admin";
        public const string MEDIC = "lekarz";
        public static readonly string[] ROLES = { PATIENT, ADMIN, MEDIC };
    }
}
