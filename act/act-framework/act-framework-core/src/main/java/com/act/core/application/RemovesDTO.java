package com.act.core.application;

import lombok.Data;

import java.util.ArrayList;
import java.util.List;

@Data
public class RemovesDTO {
    private List<Long> ids = new ArrayList<>();
}
