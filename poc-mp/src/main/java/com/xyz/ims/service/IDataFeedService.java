package com.xyz.ims.service;

import java.util.List;
import java.util.Map;

public interface IDataFeedService {

    void populateInMemoryDataStructure(Map<Integer, Map<Integer, Map<Integer, Integer>>> mp, List<Integer> users, List<Integer>days, List<Integer> activities);
}
