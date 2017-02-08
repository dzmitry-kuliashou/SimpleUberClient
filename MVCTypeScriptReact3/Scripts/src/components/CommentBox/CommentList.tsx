import * as React from "react";
import {Comment} from "./Comment";
import {IComment} from "./Comment";

export interface ICommentListProps {
    data: IComment[]
}

export class CommentList extends React.Component<ICommentListProps, {}>{
    constructor(props: ICommentListProps, context: any) {
        super(props, context);
    }

    render() {
        let commentNodes = this.props.data.map(function (comment : IComment) {
            return (
                <Comment author={comment.Author}>
                    {comment.Text}
                </Comment>
            );
        });
        return (
            <div className="commentList">
                {commentNodes}
            </div>
        );
    }
}


