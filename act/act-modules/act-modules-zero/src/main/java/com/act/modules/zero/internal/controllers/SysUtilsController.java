package com.act.modules.zero.internal.controllers;

import com.act.core.utils.AjaxResponse;
import com.act.core.utils.FriendlyException;
import com.alibaba.fastjson.JSON;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.Data;
import lombok.var;
import org.springframework.boot.system.ApplicationHome;
import org.springframework.http.MediaType;
import org.springframework.util.ResourceUtils;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import javax.servlet.http.HttpServletRequest;
import java.io.File;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.*;

@Api(tags = "系统工具")
@RestController
@RequestMapping("/api/v1/utils/")
@SuppressWarnings("all")
public class SysUtilsController {

    @ApiOperation(value = "上传图片")
    @PostMapping(value = "uploadImages", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public AjaxResponse<Object> uploadImage(@RequestParam("files") MultipartFile[] files) throws IOException {
        var filePaths = new ArrayList<String>();

        var calendar = Calendar.getInstance();
        var webRootPath = ResourceUtils.getFile("classpath:").getPath() + "/static/";
        var path = calendar.get(Calendar.YEAR) + "/" +
                calendar.get(Calendar.YEAR) + calendar.get(Calendar.MONTH) + "/" +
                calendar.get(Calendar.YEAR) + calendar.get(Calendar.MONTH) + calendar.get(Calendar.DATE) + "/";

        var directory = new File(webRootPath + path);
        if (!directory.exists())
            directory.mkdirs();

        for (var file : files) {
            if (file == null)
                continue;

            //文件后缀
            var suffix = file.getOriginalFilename().substring(file.getOriginalFilename().lastIndexOf("."));
            //上传文件名
            var date = new Date();
            var formate = new SimpleDateFormat("yyyyMMddHHmmss");
            var strDateTime = formate.format(date);
            var strRan = (int) (Math.random() * 900 + 100);
            var saveName = strDateTime + strRan + suffix;

            var filePath = new File(webRootPath + path + saveName);
            file.transferTo(filePath);
            var fileAllPath = (path + saveName);
            filePaths.add(fileAllPath);
        }

        return new AjaxResponse<Object>(JSON.toJSONString(filePaths), "上传成功", 200);
    }
}
