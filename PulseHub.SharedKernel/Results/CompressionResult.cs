﻿namespace PulseHub.SharedKernel.Results;

public record CompressionResult(string filename, string path, long totalSize, int height, int width,string format);
