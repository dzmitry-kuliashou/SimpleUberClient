import * as React from "react";
import * as ReactDOM from "react-dom";
import { CommentBox } from "./components/CommentBox/CommentBox";

ReactDOM.render(
    <CommentBox url="/comments" submitUrl="/comments/new" loginUrl="/login" pollInterval={2000} />,
    document.getElementById("content")
);
