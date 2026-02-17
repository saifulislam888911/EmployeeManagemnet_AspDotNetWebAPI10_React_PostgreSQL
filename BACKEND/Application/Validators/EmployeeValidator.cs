using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.NID)
            .NotEmpty().WithMessage("NID is required")
            .Must(BeValidNID).WithMessage("NID must be 10 or 17 digits");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .Must(BeValidBDPhone).WithMessage("Phone must be in BD format (+880XXXXXXXXX or 01XXXXXXXXX)");

        RuleFor(x => x.Department)
            .NotEmpty().WithMessage("Department is required");

        RuleFor(x => x.BasicSalary)
            .GreaterThan(0).WithMessage("Basic salary must be greater than 0");
            
        When(x => x.Spouse != null, () =>
        {
            RuleFor(x => x.Spouse!.Name)
                .NotEmpty().WithMessage("Spouse name is required");
            RuleFor(x => x.Spouse!.NID)
                .NotEmpty().WithMessage("Spouse NID is required")
                .Must(BeValidNID).WithMessage("Spouse NID must be 10 or 17 digits");
        });
        
        RuleForEach(x => x.Children).ChildRules(child =>
        {
            child.RuleFor(c => c.Name).NotEmpty().WithMessage("Child name is required");
            child.RuleFor(c => c.DateOfBirth).LessThan(DateTime.Now).WithMessage("Child date of birth must be in the past");
        });
    }

    private bool BeValidNID(string nid)
    {
        return nid.Length == 10 && nid.All(char.IsDigit) || 
               nid.Length == 17 && nid.All(char.IsDigit);
    }

    private bool BeValidBDPhone(string phone)
    {
        // +8801[3-9]XXXXXXXX (14 chars) or 01[3-9]XXXXXXXX (11 chars)
        // BD operators use: 013, 014, 015, 016, 017, 018, 019
        if (phone.StartsWith("+8801") && phone.Length == 14)
        {
            // Check 5th character (after +8801) is between 3-9
            if (phone[4] >= '3' && phone[4] <= '9' && phone.Substring(1).All(char.IsDigit))
                return true;
        }
        if (phone.StartsWith("01") && phone.Length == 11)
        {
            // Check 3rd character (after 01) is between 3-9
            if (phone[2] >= '3' && phone[2] <= '9' && phone.All(char.IsDigit))
                return true;
        }
        return false;
    }
}

public class UpdateEmployeeDtoValidator : AbstractValidator<UpdateEmployeeDto>
{
    public UpdateEmployeeDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.NID)
            .NotEmpty().WithMessage("NID is required")
            .Must(BeValidNID).WithMessage("NID must be 10 or 17 digits");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .Must(BeValidBDPhone).WithMessage("Phone must be in BD format (+880XXXXXXXXX or 01XXXXXXXXX)");

        RuleFor(x => x.Department)
            .NotEmpty().WithMessage("Department is required");

        RuleFor(x => x.BasicSalary)
            .GreaterThan(0).WithMessage("Basic salary must be greater than 0");
            
        When(x => x.Spouse != null, () =>
        {
            RuleFor(x => x.Spouse!.Name)
                .NotEmpty().WithMessage("Spouse name is required");
            RuleFor(x => x.Spouse!.NID)
                .NotEmpty().WithMessage("Spouse NID is required")
                .Must(BeValidNID).WithMessage("Spouse NID must be 10 or 17 digits");
        });
        
        RuleForEach(x => x.Children).ChildRules(child =>
        {
            child.RuleFor(c => c.Name).NotEmpty().WithMessage("Child name is required");
            child.RuleFor(c => c.DateOfBirth).LessThan(DateTime.Now).WithMessage("Child date of birth must be in the past");
        });
    }

    private bool BeValidNID(string nid)
    {
        return nid.Length == 10 && nid.All(char.IsDigit) || 
               nid.Length == 17 && nid.All(char.IsDigit);
    }

    private bool BeValidBDPhone(string phone)
    {
        // +8801[3-9]XXXXXXXX (14 chars) or 01[3-9]XXXXXXXX (11 chars)
        // BD operators use: 013, 014, 015, 016, 017, 018, 019
        if (phone.StartsWith("+8801") && phone.Length == 14)
        {
            // Check 5th character (after +8801) is between 3-9
            if (phone[4] >= '3' && phone[4] <= '9' && phone.Substring(1).All(char.IsDigit))
                return true;
        }
        if (phone.StartsWith("01") && phone.Length == 11)
        {
            // Check 3rd character (after 01) is between 3-9
            if (phone[2] >= '3' && phone[2] <= '9' && phone.All(char.IsDigit))
                return true;
        }
        return false;
    }
}
