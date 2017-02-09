import * as React from "react";
import * as ReactDOM from "react-dom";

export interface IErrorBoxProps {
    isVisible: boolean,
    errorMessage : string
}

export class ErrorBox extends React.Component<IErrorBoxProps, {}>{
    constructor(props: IErrorBoxProps, context: any) {
        super(props, context);
    }

    render() {
        const visibleStyle = {
            visibility: "visible",
            borderStyle: "solid",
            borderColor: "#b94a48",
            borderRadius: 5,
            padding: "5px",
            display: "inline-block"
        };

        const invisibleStyle = {
            visibility: "hidden"
        };

        let errorMessages = this.props.errorMessage.split("|");

        let errors = errorMessages.map(function (errorMessage: string) {
            return (
                <div>{errorMessage}</div>
            );
        });

        return (
            <div style={this.props.isVisible ? visibleStyle : invisibleStyle}>
                {errors}
            </div>
        );
    }
}