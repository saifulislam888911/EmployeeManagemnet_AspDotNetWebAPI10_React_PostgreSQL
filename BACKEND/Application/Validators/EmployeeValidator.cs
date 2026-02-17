using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.NID).NotEmpty().WithMessage("NID is required").Must(BeValidNID).WithMessage("NID must be 10 or 17 digits");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required").Must(BeValidBDPhone).WithMessage("Phone must be in BD format (+880 or 01)");
        RuleFor(x => x.Department).NotEmpty().WithMessage("Department is required");
        RuleFor(x => x.BasicSalary).GreaterThan(0).WithMessage("Basic salary must be greater than 0");
        When(x => x.Spouse != null, () =>
        {
            RuleFor(x => x.Spouse!.Name).NotEmpty().WithMessage("Spouse name is required");
            RuleFor(x => x.Spouse!.NID).NotEmpty().WithMessage("Spouse NID is required").Must(BeValidNID).WithMessage("Spouse NID must be 10 or 17 digits");
        });
        RuleForEach(x => x.Children).ChildRules(child =>
        {
            child.RuleFor(c => c.Name).NotEmpty().WithMessage("Child name is required");
            child.RuleFor(c => c.DateOfBirth).LessThan(DateTime.Now).WithMessage("Child date of birth must be in the past");
        });
    }

    private static bool BeValidNID(string nid) =>
        (nid.Length == 10 || nid.Length == 17) && nid.All(char.IsDigit);

    private static bool BeValidBDPhone(string phone)
    {
        if (phone.StartsWith("+8801") && phone.Length == 14)
            return phone[4] >= '3' && phone[4] <= '9' && phone.Substring(1).All(char.IsDigit);
        if (phone.StartsWith("01") && phone.Length == 11)
            return phone[2] >= '3' && phone[2] <= '9' && phone.All(char.IsDigit);
        return false;
    }
}

public class UpdateEmployeeDtoValidator : AbstractValidator<UpdateEmployeeDto>
{
    public UpdateEmployeeDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.NID).NotEmpty().WithMessage("NID is required").Must(BeValidNID).WithMessage("NID must be 10 or 17 digits");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required").Must(BeValidBDPhone).WithMessage("Phone must be in BD format (+880 or 01)");
        RuleFor(x => x.Department).NotEmpty().WithMessage("Department is required");
        RuleFor(x => x.BasicSalary).GreaterThan(0).WithMessage("Basic salary must be greater than 0");
        When(x => x.Spouse != null, () =>
        {
            RuleFor(x => x.Spouse!.Name).NotEmpty().WithMessage("Spouse name is required");
            RuleFor(x => x.Spouse!.NID).NotEmpty().WithMessage("Spouse NID is required").Must(BeValidNID).WithMessage("Spouse NID must be 10 or 17 digits");
        });
        RuleForEach(x => x.Children).ChildRules(child =>
        {
            child.RuleFor(c => c.Name).NotEmpty().WithMessage("Child name is required");
            child.RuleFor(c => c.DateOfBirth).LessThan(DateTime.Now).WithMessage("Child date of birth must be in the past");
        });
    }

    private static bool BeValidNID(string nid) =>
        (nid.Length == 10 || nid.Length == 17) && nid.All(char.IsDigit);

    private static bool BeValidBDPhone(string phone)
    {
        if (phone.StartsWith("+8801") && phone.Length == 14)
            return phone[4] >= '3' && phone[4] <= '9' && phone.Substring(1).All(char.IsDigit);
        if (phone.StartsWith("01") && phone.Length == 11)
            return phone[2] >= '3' && phone[2] <= '9' && phone.All(char.IsDigit);
        return false;
    }
}
