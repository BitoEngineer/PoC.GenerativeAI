# PoC.GenerativeAI

Welcome to the `PoC.GenerativeAI` repository! This project is a collection of Proof-of-Concepts (PoCs) showcasing how Generative AI can be integrated into software solutions. The focus is on practical applications of AI models to enhance and automate tasks that typically require human intervention.

Check out more details on the Medium article - [Leverage Generative AI on Software Solutions](https://nuno-gp-cardoso.medium.com/leveraging-generative-ai-in-software-solutions-9a2e25c3c123)

## Key Features

- **Article Analysis**: Automatically interpret raw text articles and extract structured insights such as title, target audience, relevant industries, humor score, relevance score, and quality score.
- **AI-Generated Images**: Generate visually appealing images that match the content of the articles using OpenAI's image generation models.
- **Flexible LLM Integration**: The solution is designed to be adaptable to different Large Language Models (LLMs), allowing easy switching between AI providers like OpenAI, Google's Gemini, Meta's Llama, etc.

## Project Structure

- **InsightsExtractorAPI**: The main project containing the .NET Minimal API for article analysis.
- **LLMClient**: A wrapper for interacting with different LLMs, implemented with dependency injection for flexibility.
- **ImageGenerator**: A module to generate images based on the article content using AI models like DALL-E.

## Getting Started

### Prerequisites

- .NET SDK installed on your machine.
- Docker (optional, for containerized deployment).
- An OpenAI API key or other compatible LLM API key.

### Running Locally

#### Clone the repository

   ```bash
   git clone https://github.com/yourusername/PoC.GenerativeAI.git
   cd PoC.GenerativeAI/PoC.GenerativeAI.InsightsExtractorAPI
   ```
   
#### Run the API locally

   ```bash
   dotnet run
   ```

#### Run with docker container

   ```bash
   cd /path/to/PoC.GenerativeAI
   docker build -t insightsextractorapi .
   docker run -d -p 8080:8080 --name insightsextractorapi-container insightsextractorapi
   ```

### Usage
Once the API is running, you can analyze articles by sending a POST request to the /analyze/article endpoint. You can include an AI-generated image in the response by adding the *includeImage=true* query parameter.

#### Example curl request:

   ```bash
   curl -X POST "http://localhost:8080/analyze/article?includeImage=true" \
    -H "Content-Type: application/json" \
    -H "OPENAI_API_KEY: your-api-key" \
    -H "OPENAI_LLM_MODEL: gpt-4o" \
    -d "{ \"content\": \"Evolving AI: Amazons Titan Image Generator v2 for AWS brings enhanced image editing and generation capabilities, ensuring more creative control for users.\n\nKey Points:\nNew capabilities include image editing, background removal, and generating image variations.\n\nUsers can guide the model with reference images for consistent aesthetics.\n\nAWS remains vague about training data but offers indemnification for potential copyright issues.\n\nDetails:\nAmazons Titan Image Generator v2, available via AWSs Bedrock platform, introduces several advanced features. Users can now guide image creation with reference images, edit visuals, and remove backgrounds. The models ability to detect and segment foreground objects, generate color-conditioned images, and maintain a consistent aesthetic with reference images like logos is noteworthy. Despite the new features, AWS remains opaque about its training data, which combines proprietary and licensed sources. This lack of transparency is common in the industry, where data is a competitive advantage. To mitigate potential copyright issues, AWS offers indemnification for customers if the model unintentionally replicates copyrighted material.\n\nThe Relevance:\nAmazons upgraded Titan Image Generator underscores the rapid evolution and growing importance of generative AI in creative industries. By enhancing user control and offering protections against copyright concerns, AWS positions itself as a leader in AI-driven innovation. As generative AI continues to expand, such advancements will likely drive significant changes in how businesses create and manage visual content, emphasizing the need for robust and transparent AI tools.\"}"
   ```

If you found this project useful, check out the series [Mastering Software Architecture](https://nuno-gp-cardoso.medium.com/mastering-software-architecture-introduction-a1de7082b232) where I regularly publish articles on technologies, frameworks, tools, and patterns relevant to software architecture and their real-life applications.

Peace ✌️
