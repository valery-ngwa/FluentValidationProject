# FluentValidationProject
trying to do validation usind INotifyDataErrorInfo, FluentValidation and Prism 
Sir i have been trying to do validations using fluentValidation with Prism
I found an example here https://www.domysee.com/blogposts/validating-wpf-viewmodels
I could not get it working so I decided to play with it and had something working.
I can now do the validation but here is another problem I encountered

Problem 1:
I have two buttons and want one of the buttons to be enebled when a particular property is valid
and the other one when the model is valid. I can do either of them but not both.

I think the problem may be coming from SetValidationErro funtion in the ValidableBindableBase.cs


public void SetValidationErrors(IEnumerable<Error> errors, string pname)
        {
            this.errors.Clear();
            foreach (var error in errors)
            {
                
                this.errors.Add(error.PropertyName, error.ErrorMessage);
            }

            //signal error change for all properties
            var properties = getProperties();
            foreach (var property in properties)
            {

                if (property==pname)
                {
                    signalErrorChange(property);
                }
            }
            
        }
        
Problem 2:
I know the code can be better than this. 

Problem 3:
Is it possible to use it through dependency injection? I mean regidter it as singleton
