import * as React from "react";
import * as ReactDOM from "react-dom";

type FormSubmitFunction = () => void

export interface ILoginFormProps {
    isLogged: boolean,
    onLoginSubmit: FormSubmitFunction
}

export class LoginForm extends React.Component<ILoginFormProps, {}>{
    constructor(props: ILoginFormProps, context: any) {
        super(props, context);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    updateState(isLogged: boolean) {
        this.setState({
            isLogged: isLogged
        });
    }

    handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        this.props.onLoginSubmit();
        return;
    }

    render() {
        let loggedText: string;
        if (this.props.isLogged)
            loggedText = "Logged";
        else
            loggedText = "Unlogged";
        return (
            <div className="loginBox">
                <form className="commentForm" onSubmit={this.handleSubmit} >
                    <input type="submit" value="Login" />
                    <span>
                        {loggedText}
                    </span>
                </form>
            </div>
        );
    }
}