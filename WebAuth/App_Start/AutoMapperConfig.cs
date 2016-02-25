using System.ComponentModel;
using WebAuth.Models;


namespace WebAuth
{
    /// <summary>
    /// AutoMapperProfile
    /// </summary>
    [Description("AutoMapper配置")]
    public class AutoMapperProfile : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<ApplicationPermission, PermissionViewModel>();
            CreateMap<PermissionViewModel, ApplicationPermission>();
            CreateMap<ApplicationRole, RoleViewModel>();
            CreateMap<RoleViewModel, ApplicationRole>()
                .ForMember(
                            dest => dest.Id,
                            sour =>
                            {
                                sour.MapFrom(s => s.Id ?? System.Guid.NewGuid().ToString());
                            });

            CreateMap<ApplicationUser, EditUserRoleViewModel>();
            CreateMap<EditUserRoleViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, EditUserDepartmentViewModel>();
            CreateMap<RegisterViewModel, ApplicationUser>();
            CreateMap<Department, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, Department>();
        }
    }

    /// <summary>
    /// AutoMapperConfig
    /// </summary>
    [Description("AutoMapper匹配")]
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
        }
    }
}