import * as React from "react";
import * as ReactDOM from "react-dom";
import {CommentList} from "./CommentList";
import {CommentForm} from "./CommentForm";
import {LoginForm} from "../Login/LoginForm";
import {IComment} from "./Comment";

export interface ICommentBoxProps {
    url : string,
    submitUrl : string,
    pollInterval: number,
    loginUrl : string
}

export interface ICommentBoxState {
    isLogged: boolean;
    data: CommentModel[]
}

class CommentModel implements IComment {
    Author : string;
    Text : string
}

export class CommentBox extends React.Component<ICommentBoxProps, ICommentBoxState>{
    constructor(props: ICommentBoxProps, context: ICommentBoxState) {
        super(props, context);
        this.handleCommentSubmit = this.handleCommentSubmit.bind(this);
        this.loadCommentsFromServer = this.loadCommentsFromServer.bind(this);
        this.handleLoginSubmit = this.handleLoginSubmit.bind(this);

        let data : CommentModel[] = [];
        this.state = {
            data: data,
            isLogged : false
        }

    }

    loadCommentsFromServer() {
        let xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);

        xhr.onload = function () {
            let data = JSON.parse(xhr.responseText);
            let isLogged = this.state.isLogged;

            this.setState({ data : data, isLogged : isLogged });
        }.bind(this);

        xhr.send();
    }

    handleCommentSubmit(comment: IComment) {
        let data = new FormData();
        data.append('Author', comment.Author);
        data.append('Text', comment.Text);

        let xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitUrl, true);

        xhr.onload = function () {
            if (xhr.responseText == "unauthorized") {
                let data = this.state.data;
                let isLogged = false;

                this.setState({ data: data, isLogged: isLogged }, function () { this.loadCommentsFromServer(); });
            }
            this.loadCommentsFromServer();
        }.bind(this);

        xhr.send(data);
    }

    handleLoginSubmit() {
        let data = new FormData();
        let xhr = new XMLHttpRequest();
        xhr.open('post', this.props.loginUrl, true);

        xhr.onload = function () {
            let data = this.state.data;
            let isLogged: boolean;

            if (xhr.responseText == "ok")
                isLogged = true;
            else
                isLogged = false;

            this.setState({ data: data, isLogged: isLogged });
        }.bind(this);

        xhr.send(data);
    }

    componentDidMount() {
        this.loadCommentsFromServer();
        window.setInterval(this.loadCommentsFromServer, this.props.pollInterval);
    }

    render() {
        return (
            <div className="commentBox">
                <LoginForm onLoginSubmit={this.handleLoginSubmit} isLogged={this.state.isLogged} />
                <h1>Comments</h1>
                <CommentList data={this.state.data} />
                <CommentForm onCommentSubmit={this.handleCommentSubmit} />
            </div>
        );
    }
}