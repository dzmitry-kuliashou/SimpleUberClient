import * as React from "react";
import * as ReactDom from "react-dom"
import {IComment} from "./Comment";

type FormSubmitFunction = (comment : IComment) => void

export interface ICommentFormProps {
    onCommentSubmit: FormSubmitFunction
}

export class CommentForm extends React.Component<ICommentFormProps, {}>{
    constructor(props: ICommentFormProps, context: any) {
        super(props, context);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    private authorInput: HTMLInputElement;

    handleSubmit(e: React.FormEvent) {
        e.preventDefault();

        let authorInput = this.authorInput;
        let textInput = ReactDom.findDOMNode<HTMLInputElement>(this.refs["text"]);
        

        let author = authorInput.value.trim();
        let text = textInput.value.trim();

        if (!text || !author) {
            return;
        }

        this.props.onCommentSubmit({ Author: author, Text: text });
        authorInput.value = '';
        textInput.value = '';
        return;
    }

    render() {
        return (
            <form className="commentForm" onSubmit={this.handleSubmit}>
                <div>This is CommentForm</div>
                <input type="text" placeholder="Your name" ref={(ref) => this.authorInput = ref } />
                <input type="text" placeholder="Say something..." ref="text" />
                <input type="submit" value="Post" />
            </form>
        );
    }
}
