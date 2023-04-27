package com.act.core.utils;

import lombok.Data;

@Data
public class FriendlyException extends RuntimeException {
    private int code;
    private String msg;

    public FriendlyException(String message) {
        msg = message;
        code = -1;
    }

    public FriendlyException(String message, int state) {
        msg = message;
        code = state;
    }


    public FriendlyException() {

    }
}
