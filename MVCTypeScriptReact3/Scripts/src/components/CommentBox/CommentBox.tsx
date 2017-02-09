import * as React from "react";
import * as ReactDOM from "react-dom";
import {CommentList} from "./CommentList";
import {CommentForm} from "./CommentForm";
import {LoginForm} from "../Login/LoginForm";
import {ErrorBox} from "../ErrorBox/ErrorBox";
import {IComment} from "./Comment";

export interface ICommentBoxProps {
    url : string,
    submitUrl : string,
    pollInterval: number,
    loginUrl : string
}

export interface ICommentBoxState {
    isLogged: boolean,
    data: CommentModel[],
    isErrorVisible: boolean,
    errorMessage: string
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
            isLogged: false,
            isErrorVisible: false,
            errorMessage: ""
        }

    }

    loadCommentsFromServer() {
        let xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);

        xhr.onload = function () {
            let data = JSON.parse(xhr.responseText);
            let isLogged = this.state.isLogged;
            let isErrorVisible = this.state.isErrorVisible;
            let errorMessage = this.state.errorMessage;

            this.setState({ data: data, isLogged: isLogged, isErrorVisible: isErrorVisible, errorMessage: errorMessage });
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
            if (xhr.responseText == "ok") {
                let data = this.state.data;
                let isLogged = this.state.isLogged;
                let isErrorVisible = false;
                let errorMessage = "";

                this.setState({ data: data, isLogged: isLogged, isErrorVisible: isErrorVisible, errorMessage: errorMessage });
            }
            else {
                this.handleUnsucceedResponse(xhr.responseText);
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
            if (xhr.responseText == "ok") {
                let data = this.state.data;
                let isLogged = true;
                let isErrorVisible = false;
                let errorMessage = "";

                this.setState({ data: data, isLogged: isLogged, isErrorVisible: isErrorVisible, errorMessage: errorMessage });

                this.loadCommentsFromServer();
            }
            else {
                this.handleUnsucceedResponse(xhr.responseText);
            }
        }.bind(this);

        xhr.send(data);
    }

    componentDidMount() {
        this.loadCommentsFromServer();
        window.setInterval(this.loadCommentsFromServer, this.props.pollInterval);
    }

    handleUnsucceedResponse(responseText: string) {
        let data: CommentModel[];
        let isLogged: boolean;
        let isErrorVisible: boolean;
        let errorMessage: string;

        if (responseText == "unauthorized") {
            data = this.state.data;
            isLogged = false;
            isErrorVisible = false;
            errorMessage = "";
        }
        else {
            data = this.state.data;
            isLogged = this.state.isLogged;
            isErrorVisible = true;
            errorMessage = responseText;
        }

        this.setState({ data: data, isLogged: isLogged, isErrorVisible: isErrorVisible, errorMessage: errorMessage });
    }

    render() {
        return (
            <div className="commentBox">
                <LoginForm onLoginSubmit={this.handleLoginSubmit} isLogged={this.state.isLogged} />
                <ErrorBox isVisible={this.state.isErrorVisible} errorMessage={this.state.errorMessage} />
                <h1>Comments</h1>
                <CommentList data={this.state.data} />
                <CommentForm onCommentSubmit={this.handleCommentSubmit} />
            </div>
        );
    }
}