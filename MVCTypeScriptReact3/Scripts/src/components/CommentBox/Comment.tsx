import * as React from "react";
import * as showdown from "showdown"

export interface ICommentProps {
    author: string
}

export interface IComment {
    Author: string,
    Text: string
}
 
export class Comment extends React.Component<ICommentProps, {}>{

    constructor(props: ICommentProps, context: any) {
        super(props, context);
    }

    render() {
        let converter = new showdown.Converter();
        let rawMarkup = converter.makeHtml(this.props.children.toString());
        return (
            <div className="comment">
                <h2 className="commentAuthor">
                    {this.props.author}
                </h2>
                <span dangerouslySetInnerHTML={{ __html: rawMarkup }} />
            </div>
        );
    }
}

