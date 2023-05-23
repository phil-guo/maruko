package com.act.core.handler;

import com.act.core.utils.AjaxResponse;
import com.act.core.utils.FriendlyException;
import io.jsonwebtoken.ExpiredJwtException;
import lombok.var;
import org.springframework.http.HttpStatus;
import org.springframework.validation.BindException;
import org.springframework.validation.FieldError;
import org.springframework.validation.ObjectError;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.RestControllerAdvice;
import org.springframework.web.multipart.MaxUploadSizeExceededException;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

/**
 * 全局异常拦截器
 */
@SuppressWarnings("all")
public class GlobalExceptionHandler {
    @ExceptionHandler(value = Exception.class)
    public AjaxResponse<Object> exceptionHandler(HttpServletRequest request, Exception e) {
        // 本地自定义异常处理
        if (e instanceof FriendlyException) {
            var friendlyException = ((FriendlyException) e);
            return new AjaxResponse<>(friendlyException.getMsg(), friendlyException.getCode());//将具体错误信息设置到msg中返回
        } else if (e instanceof ExpiredJwtException) {
            return new AjaxResponse<>("token 过期！", -1);
        } else if (e instanceof MaxUploadSizeExceededException) {
            return new AjaxResponse<>("上传文件大小不能超过500M!", -1);
        }
        //绑定异常是需要明确提示给用户的
        else if (e instanceof BindException) {
            BindException exception = (BindException) e;
            List<ObjectError> errors = exception.getAllErrors();
            String msg = errors.get(0).getDefaultMessage();//获取自错误信息
            return new AjaxResponse<>(msg, -1);//将具体错误信息设置到msg中返回
        } else {
            return new AjaxResponse<>(Arrays.stream(e.getStackTrace()).findFirst().toString(), -1);//将具体错误信息设置到msg中返回
        }
    }

    /**
     * 空指针异常
     *
     * @param request
     * @param e
     * @return
     */
    @ExceptionHandler(value = NullPointerException.class)
    public AjaxResponse<Object> nullHandler(HttpServletRequest request, NullPointerException e) {
        //将具体错误信息设置到msg中返回
        var friendlyException = new FriendlyException(Arrays.stream(e.getStackTrace()).findFirst().toString());
        return new AjaxResponse<>("NULL 指针异常[" + friendlyException.getMsg() + "]", friendlyException.getCode());
    }

    // <2> 处理 json 请求体调用接口校验失败抛出的异常
    @ExceptionHandler(MethodArgumentNotValidException.class)
    public AjaxResponse<Object> methodArgumentNotValidExceptionHandler(HttpServletResponse httpServletResponse, MethodArgumentNotValidException e) {
        List<FieldError> fieldErrors = e.getBindingResult().getFieldErrors();
        List<String> collect = fieldErrors.stream()
                .map(o -> o.getDefaultMessage())
                .collect(Collectors.toList());
        return new AjaxResponse<Object>(collect.toString(), HttpStatus.BAD_REQUEST.value());
    }
}
