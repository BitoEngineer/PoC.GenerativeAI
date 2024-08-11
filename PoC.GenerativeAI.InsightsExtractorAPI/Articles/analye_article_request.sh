﻿#!/bin/bash
# chmod +x analyze_article.sh

curl -X POST "https://localhost:5001/analyze/article?includeImage=true" -H "Content-Type: application/json" -H "OPENAI_API_KEY: your-api-key" -H "OPENAI_LLM_MODEL: gpt-4o" -d "{ \"content\": \"Evolving AI: Amazons Titan Image Generator v2 for AWS brings enhanced image editing and generation capabilities, ensuring more creative control for users.\n\nKey Points:\nNew capabilities include image editing, background removal, and generating image variations.\n\nUsers can guide the model with reference images for consistent aesthetics.\n\nAWS remains vague about training data but offers indemnification for potential copyright issues.\n\nDetails:\nAmazons Titan Image Generator v2, available via AWSs Bedrock platform, introduces several advanced features. Users can now guide image creation with reference images, edit visuals, and remove backgrounds. The models ability to detect and segment foreground objects, generate color-conditioned images, and maintain a consistent aesthetic with reference images like logos is noteworthy. Despite the new features, AWS remains opaque about its training data, which combines proprietary and licensed sources. This lack of transparency is common in the industry, where data is a competitive advantage. To mitigate potential copyright issues, AWS offers indemnification for customers if the model unintentionally replicates copyrighted material.\n\nThe Relevance:\nAmazons upgraded Titan Image Generator underscores the rapid evolution and growing importance of generative AI in creative industries. By enhancing user control and offering protections against copyright concerns, AWS positions itself as a leader in AI-driven innovation. As generative AI continues to expand, such advancements will likely drive significant changes in how businesses create and manage visual content, emphasizing the need for robust and transparent AI tools.\"}"