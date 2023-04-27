package com.act.core.utils;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class AjaxResponse<T> {
    public AjaxResponse(T result, String message, int status) {
        data = result;
        msg = message;
        code = status;
    }

    public AjaxResponse(String message, int status) {
        data = null;
        msg = message;
        code = status;
    }

    public AjaxResponse(T result) {
        data = result;
        msg = "ok";
        code = 200;
    }

    public AjaxResponse() {
    }

    private int code;
    private String msg = "";
    private T data;
}
